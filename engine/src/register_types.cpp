#include "FileControlor/FileControlor/FileControlor.h"

#include <godot_cpp/core/class_db.hpp>
#include <godot_cpp/core/defs.hpp>
#include <godot_cpp/godot.hpp>

using namespace godot;

// 模块初始化：注册所有 C++ 类
void initialize_reaction_module(ModuleInitializationLevel p_level) {
    if (p_level != MODULE_INITIALIZATION_LEVEL_SCENE) {
        return;
    }
    ClassDB::register_class<FileControlor::FileController>();
    // 后续在这里继续 register_class<reaction::Compositor>() 等
}

// 模块卸载（通常留空）
void uninitialize_reaction_module(ModuleInitializationLevel p_level) {
    if (p_level != MODULE_INITIALIZATION_LEVEL_SCENE) {
        return;
    }
}

// ★ Godot 4 GDExtension 的入口签名（宏自动生成 extern "C" 导出）
extern "C" {
    GDExtensionBool GDE_EXPORT reaction_library_init(
        GDExtensionInterfaceGetProcAddress p_get_proc_address,
        const GDExtensionClassLibraryPtr p_library,
        GDExtensionInitialization* r_initialization) {

        GDExtensionBinding::InitObject init_obj(p_get_proc_address,
            p_library, r_initialization);

        init_obj.register_initializer(initialize_reaction_module);
        init_obj.register_terminator(uninitialize_reaction_module);
        init_obj.set_minimum_library_initialization_level(
            MODULE_INITIALIZATION_LEVEL_SCENE);

        return init_obj.init();
    }
}