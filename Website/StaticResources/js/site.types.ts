/// <reference path="croppie.d.ts"/>

enum CroppieViewportType {
    square,
    circle
}

class CroppieViewport implements ICroppieViewport {
    constructor(width: number, height: number, type: CroppieViewportType) {
        this.width = width;
        this.height = height;
        this.type = CroppieViewportType[type];
    }

    width: number;
    height: number;
    type: string;
}
