/// <reference path="croppie.d.ts"/>
var CroppieViewportType;
(function (CroppieViewportType) {
    CroppieViewportType[CroppieViewportType["square"] = 0] = "square";
    CroppieViewportType[CroppieViewportType["circle"] = 1] = "circle";
})(CroppieViewportType || (CroppieViewportType = {}));
var CroppieViewport = (function () {
    function CroppieViewport(width, height, type) {
        this.width = width;
        this.height = height;
        this.type = CroppieViewportType[type];
    }
    return CroppieViewport;
}());
//# sourceMappingURL=site.types.js.map