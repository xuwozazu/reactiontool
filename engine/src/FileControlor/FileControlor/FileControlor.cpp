#include "FileControlor.h"

#include <fstream>

#include <godot_cpp/classes/project_settings.hpp>
#include <godot_cpp/core/class_db.hpp>
#include <godot_cpp/variant/utility_functions.hpp>

namespace FileControlor {

namespace {

godot::String to_native_path(const godot::String &file_path) {
	godot::ProjectSettings *settings = godot::ProjectSettings::get_singleton();
	if (settings != nullptr) {
		return settings->globalize_path(file_path);
	}
	return file_path;
}

} // namespace

FileController::FileController() {}

FileController::~FileController() {}

bool FileController::create_file(const godot::String &file_path) {
	const godot::String native_path = to_native_path(file_path);
	std::ofstream file(native_path.utf8().get_data());
	if (file.is_open()) {
		file.close();
		godot::UtilityFunctions::print("File created successfully: ", native_path);
		return true;
	}

	godot::UtilityFunctions::printerr("Failed to create file: ", native_path);
	return false;
}

bool FileController::file_exists(const godot::String &file_path) const {
	const godot::String native_path = to_native_path(file_path);
	std::ifstream file(native_path.utf8().get_data());
	return file.good();
}

void FileController::_bind_methods() {
	godot::ClassDB::bind_method(godot::D_METHOD("create_file", "file_path"), &FileController::create_file);
	godot::ClassDB::bind_method(godot::D_METHOD("file_exists", "file_path"), &FileController::file_exists);
}

} // namespace FileControlor
