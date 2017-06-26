/* 
This file in the main entry point for defining grunt tasks and using grunt plugins. 
Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409 
*/
module.exports = function (grunt) {
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-ts');
    grunt.initConfig({
        ts:
        {
            base:
            {
                src: ['Scripts/app/boot.ts', 'Scripts/app/**/*.ts'],
                outDir: 'wwwroot/app',
                tsconfig: './tsconfig.json'
            }
        },
        uglify:
        {
            my_target:
            {
                files: [{
                    expand: true,
                    cwd: 'wwwroot/app',
                    src: ['**/*.js'],
                    dest: 'wwwroot/app'
                }]
            },
            options:
            {
                sourceMap: true
            }
        },
        // Copy all JS files from external libraries and required NPM packages to wwwroot/js    
        copy: {
            main: {
                files:
                [{
                    expand: true,
                    flatten: true,
                    src: ['Scripts/js/**/*.js', 'node_modules/es6-shim/es6-shim.min.js', 'node_modules/systemjs/dist/system-polyfills.js', 'node_modules/angular2/bundles/angular2-polyfills.js', 'node_modules/systemjs/dist/system.src.js', 'node_modules/rxjs/bundles/Rx.js', 'node_modules/angular2/bundles/angular2.dev.js'],
                    dest: 'wwwroot/js/',
                    filter: 'isFile'
                }]
            }
        },
        // Watch specified files and define what to do upon file changes    
        watch: {
            scripts: {
                files: ['Scripts/**/*.js'],
                tasks: ['ts', 'uglify', 'copy']
            }
        }
    });
    // Define the default task so it will launch all other tasks    
    grunt.registerTask('default', ['ts', 'uglify', 'copy', 'watch']);
};  