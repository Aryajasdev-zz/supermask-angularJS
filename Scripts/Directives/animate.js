var animateDirective = function ($animateCss) {
    return {
        'link': function (scope, element) {
            $animateCss(element, {
                'event': 'enter',
                structural: true
            }).start();
        }
    };
};
animateDirective.$inject = ['$animateCss'];