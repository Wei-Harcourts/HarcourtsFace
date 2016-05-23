/**
 * Configure Grunt - http://benfrain.com/lightning-fast-sass-compiling-with-libsass-node-sass-and-grunt-sass/
 * 
 * The following things are managed by Grunt:
 * sass-watch : The sass watcher which will detect changes to the sass files and compile they in the real time.
 * sass : The sass compiler which compiles sass files into proper CSS files.
 */

module.exports = function (grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        watch: {
            sass: {
                files: [
                    'StaticResources/css/{,*/}*.scss'
                ],
                tasks: ['sass:dist']
            }
        },
        sass: {
            dist: {
                options: {
                    check: false,
                    unixNewlines: true,
                    precision: 10
                },
                files: {
                    'StaticResources/css/site.css': 'StaticResources/css/site.scss'
                }
            }
        }
    });
    grunt.registerTask('default', ['sass']);
    grunt.loadNpmTasks('grunt-sass');
    grunt.loadNpmTasks('grunt-contrib-watch');
};
