#pragma once
#include <godot_cpp/classes/ref_counted.hpp>
#include <godot_cpp/core/class_db.hpp>
#include <godot_cpp/variant/string.hpp>
#include <direct.h>
#include<nlohmann/json.hpp>

namespace ProjectControlor
{
	class ProjectControler : public godot::RefCounted
	{
		GDCLASS(ProjectControler, godot::RefCounted);
	public:
		ProjectControler();
		~ProjectControler();
	public:	
		godot::String dirName;
		godot::String rootDirPath;
		bool CreateDir(const godot::String& dirPath);
		godot::List<godot::String> GetAllFilesNames(const godot::String& fileExtension = "", bool recursive = false);
		bool CreateConfigFile();


	};


}