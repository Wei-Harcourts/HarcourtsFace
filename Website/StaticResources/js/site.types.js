/// <reference path="croppie.d.ts"/>
var CroppieViewport = (function () {
    function CroppieViewport(width, height, type) {
        this.width = width;
        this.height = height;
        this.type = CroppieViewportType[type];
    }
    return CroppieViewport;
}());
//# sourceMappingURL=site.types.js.map