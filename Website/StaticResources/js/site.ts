/// <reference path="../../Scripts/typings/jquery/jquery.d.ts"/>
/// <reference path="site.types.ts"/>
"use strict";

($ => {

    $(document).ready(
        () => {
            var viewPort = new CroppieViewport(123, 123, CroppieViewportType.circle);
            console.log(viewPort);
        });

})(jQuery);