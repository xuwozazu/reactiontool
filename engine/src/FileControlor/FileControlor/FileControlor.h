#pragma once

#include <godot_cpp/classes/ref_counted.hpp>
#include <godot_cpp/core/class_db.hpp>
#include <godot_cpp/variant/string.hpp>

namespace FileControlor {

class FileController : public godot::RefCounted {
	GDCLASS(FileController, godot::RefCounted);

public:
	FileController();
	~FileController();

	bool create_file(const godot::String &file_path);
	bool file_exists(const godot::String &file_path) const;

protected:
	static void _bind_methods();
};

} // namespace FileControlor
