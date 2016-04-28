require('es6-promise').polyfill();
/// <binding AfterBuild='css, js' />
var gulp = require("gulp"),
    sass = require("gulp-sass"),
    browserSync = require("browser-sync"),
    autoprefixer = require("gulp-autoprefixer"),
    uglify = require("gulp-uglify"),
    jshint = require("gulp-jshint"),
    header = require("gulp-header"),
    rename = require("gulp-rename"),
    cssnano = require("gulp-cssnano"),
    package = require("./package.json");


var banner = [
    "/*!\n" +
    " * <%= package.name %>\n" +
    " * <%= package.title %>\n" +
    " * <%= package.url %>\n" +
    " * @author <%= package.author %>\n" +
    " * @version <%= package.version %>\n" +
    " * Copyright " +
    new Date().getFullYear() +
    ". <%= package.license %> licensed.\n" +
    " */",
    "\n"
].join("");

gulp.task("css", function () {
    return gulp.src("Styles/scss/site.scss")
        .pipe(sass({ errLogToConsole: true }))
        .pipe(autoprefixer("last 4 version"))
        .pipe(gulp.dest("Styles/css"))
        .pipe(cssnano())
        .pipe(rename({ suffix: ".min" }))
        .pipe(header(banner, { package: package }))
        .pipe(gulp.dest("Styles/css"));
});

/*gulp.task("js",function() {
    gulp.src("src/js/scripts.js")
        .pipe(jshint(".jshintrc"))
        .pipe(jshint.reporter("default"))
        .pipe(header(banner, { package: package }))
        .pipe(gulp.dest("Content/js"))
        .pipe(uglify())
        .pipe(header(banner, { package: package }))
        .pipe(rename({ suffix: ".min" }))
        .pipe(gulp.dest("Content/js"));
});*/
