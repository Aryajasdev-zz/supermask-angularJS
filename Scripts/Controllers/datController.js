var datController = function ($interval) {
    var timeController = this;
    timeController.clock = { time: "", interval: 1000 };
    $interval(function () {
        timeController.clock.time = Date.now();
    },
    timeController.clock.interval);
};

datController.$inject = ['$interval'];
