interface ICroppieViewport {
    width: number;
    height: number;
    type: string;
}

interface ICroppieOptions {
    viewport: ICroppieViewport;
}

declare enum CroppieViewportType {
    square,
    circle
}