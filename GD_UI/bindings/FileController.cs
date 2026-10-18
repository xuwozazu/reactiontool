using Godot;
using System;

/// <summary>
/// Thin C# wrapper around the GDExtension class <c>FileController</c>.
/// Godot .NET does not generate typed bindings for GDExtension classes;
/// use ClassDB + Call instead of <c>new FileController()</c>.
/// 
/// ★ 本文件演示两种 C++ → C# 回调模式：
///   1. 信号模式（CreateFileWithSignal + OnFileCreatedSucceeded/OnFileCreatedFailed 事件）
///   2. Callable 模式（CreateFileWithCallback + 回调 Action）
/// </summary>
public sealed class FileController
{
	public const string ClassName = "FileController";

	// —— 信号名常量（与 C++ 端 ADD_SIGNAL 声明保持一致）——
	private const string Signal_Succeeded = "file_created_succeeded";
	private const string Signal_Failed    = "file_created_failed";

	private readonly GodotObject _native;

	// ============================================================
	//  【回调方式 1：Godot 信号】——C# 端事件封装
	//  用法：fc.OnFileCreatedSucceeded += (msg, path) => {...};
	// ============================================================
	public event Action<string, string>? OnFileCreatedSucceeded;
	public event Action<string, string>? OnFileCreatedFailed;

	private Callable? _succeededHandler;
	private Callable? _failedHandler;
	private bool _signalWired;

	private FileController(GodotObject native)
	{
		_native = native;
	}

	public static FileController Instantiate()
	{
		if (!ClassDB.ClassExists(ClassName))
		{
			throw new InvalidOperationException(
				$"GDExtension class '{ClassName}' is not registered. Check that reaction_ext.dll loaded.");
		}

		Variant instance = ClassDB.Instantiate(ClassName);
		GodotObject obj = instance.AsGodotObject();
		if (obj is null)
		{
			throw new InvalidOperationException($"ClassDB.Instantiate(\"{ClassName}\") returned null.");
		}

		return new FileController(obj);
	}

	// —— 原有同步方法 ——
	public bool CreateFile(string filePath)
	{
		return _native.Call("create_file", filePath).AsBool();
	}

	public bool FileExists(string filePath)
	{
		return _native.Call("file_exists", filePath).AsBool();
	}

	// ============================================================
	//  【回调方式 1：Godot 信号】
	//  调用后立刻返回，实际结果通过 OnFileCreatedSucceeded / OnFileCreatedFailed 派发
	// ============================================================
	public bool CreateFileWithSignal(string filePath)
	{
		// ★ 第一个调用时，把 C++ 信号挂接到 C# Callable（只挂一次，避免重复）
		EnsureSignalWired();

		return _native.Call("create_file_with_signal", filePath).AsBool();
	}

	// ============================================================
	//  【回调方式 2：Callable 参数】
	//  直接把回调作为 Action 传进 C++，C++ 完成时 invoke
	// ============================================================
	public void CreateFileWithCallback(string filePath, Action<bool, string, string> callback)
	{
		if (callback is null)
			throw new ArgumentNullException(nameof(callback));

		// ★ C# Action → Godot Callable：通过 Callable.From(委托) 转换
		//   签名必须和 C++ 端 callp 传入的参数个数和类型完全匹配：
		//   (bool success, string message, string filePath)
		Callable callable = Callable.From(callback);

		_native.Call("create_file_with_callback", filePath, callable);
	}

	// ============================================================
	//  信号桥接：C++ emit_signal → GodotObject 信号 → C# event
	// ============================================================
	private void EnsureSignalWired()
	{
		if (_signalWired) return;

		// 信号参数与 C++ ADD_SIGNAL 一致：(string message, string file_path)
		_succeededHandler = Callable.From<string, string>((message, path) =>
		{
			OnFileCreatedSucceeded?.Invoke(message, path);
		});

		_failedHandler = Callable.From<string, string>((message, path) =>
		{
			OnFileCreatedFailed?.Invoke(message, path);
		});

		// 把 Callable 连到 native 信号上（默认 flags=0，不用 OneShot，后续多次调用仍然会收到事件）
		_native.Connect(Signal_Succeeded, _succeededHandler.Value);
		_native.Connect(Signal_Failed, _failedHandler.Value);

		_signalWired = true;
	}
}
