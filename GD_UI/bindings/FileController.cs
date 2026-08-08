using Godot;

/// <summary>
/// Thin C# wrapper around the GDExtension class <c>FileController</c>.
/// Godot .NET does not generate typed bindings for GDExtension classes;
/// use ClassDB + Call instead of <c>new FileController()</c>.
/// </summary>
public sealed class FileController
{
	public const string ClassName = "FileController";

	private readonly GodotObject _native;

	private FileController(GodotObject native)
	{
		_native = native;
	}

	public static FileController Instantiate()
	{
		if (!ClassDB.ClassExists(ClassName))
		{
			throw new System.InvalidOperationException(
				$"GDExtension class '{ClassName}' is not registered. Check that reaction_ext.dll loaded.");
		}

		Variant instance = ClassDB.Instantiate(ClassName);
		GodotObject obj = instance.AsGodotObject();
		if (obj is null)
		{
			throw new System.InvalidOperationException($"ClassDB.Instantiate(\"{ClassName}\") returned null.");
		}

		return new FileController(obj);
	}

	public bool CreateFile(string filePath)
	{
		return _native.Call("create_file", filePath).AsBool();
	}

	public bool FileExists(string filePath)
	{
		return _native.Call("file_exists", filePath).AsBool();
	}
}
