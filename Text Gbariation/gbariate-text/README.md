# Makefile

```Makefile
all: compile project
run: run executable
clean: clean object files and zip/tar
list: list all project's directories and files
zip/tar: compress project folder
update: update makefile
readme: generate this readme
create: create project structure
```


Set `debug=1` to compile/run in debug mode  
Use `CFLAGS+=flags` to add compiler flags  
Set `CC=compiler` to change compiler  
Set `NAME=name` to set project name  
Set `USER_LIBS=libraries` to set user-defined libraries  

