/*global angular */
(function (angular) {
    'use strict';
    angular.module('countdownTimer', [])
        .directive('countdown', function ($interval, dateFilter) {
            return {
                restrict: 'A',
                transclude: true,
                link: function (scope, element, attrs) {
                    var countDownInterval;

                    function displayString() {
                        
                        attrs.units = angular.isArray(attrs.units) ?  attrs.units : attrs.units.split('|');
                        var lastUnit = attrs.units[attrs.units.length - 1],
                            unitConstantForMillisecs = {
                                weeks: (1000 * 60 * 60 * 24 * 7),
                                days: (1000 * 60 * 60 * 24),
                                hours: (1000 * 60 * 60),
                                minutes: (1000 * 60),
                                seconds: 1000,
                                milliseconds: 1
                            },
                            unitsLeft = {},
                            returnString = '',
                            totalMillisecsLeft = new Date(attrs.endDate) - new Date(),
                            i,
                            unit;
                        for (i in attrs.units) {
                            //added by HM Dadou 
                            if(new Date(attrs.endDate) - new Date()<= 0) {
                                returnString = 'Countdown finished';
                            }else{

                            if (attrs.units.hasOwnProperty(i)) {
                                //validation
                                unit = attrs.units[i].trim();
                                if (unitConstantForMillisecs[unit.toLowerCase()] === false) {
                                    $interval.cancel(countDownInterval);
                                    throw new Error('Cannot repeat unit: ' + unit);

                                }
                                if (unitConstantForMillisecs.hasOwnProperty(unit.toLowerCase()) === false) {
                                    $interval.cancel(countDownInterval);
                                    throw new Error('Unit: ' + unit + ' is not supported. Please use following units: weeks, days, hours, minutes, seconds, milliseconds');
                                }

                                //saving unit left into object
                                unitsLeft[unit] = totalMillisecsLeft / unitConstantForMillisecs[unit.toLowerCase()];

                                //precise rounding
                                if (lastUnit === unit) {
                                    unitsLeft[unit] = Math.ceil(unitsLeft[unit]);
                                } else {
                                    unitsLeft[unit] = Math.floor(unitsLeft[unit]);
                                }
                                //updating total time left
                                totalMillisecsLeft -= unitsLeft[unit] * unitConstantForMillisecs[unit.toLowerCase()];
                                //setting this value to false for validation of repeated units
                                unitConstantForMillisecs[unit.toLowerCase()] = false;
                                //adding verbage
                                var percentminus = 100 - (Math.floor((unitsLeft[unit]*100)/60));

var percent = Math.floor((unitsLeft[unit]*100)/60);
var degrees = Math.floor((percentminus* 360)/100);

//console.log(degrees);
                               // returnString += '<span class="cnt-'+unit+'" id="'+unitsLeft[unit]+'"> ' + unitsLeft[unit] +'</span>';

                              //   returnString += '<percent-circle percent="'+unitsLeft[unit]+'"></percent-circle>';

//returnString +='<div class="pc-container ng-isolate-scope" percent="'+unitsLeft[unit]+'"><div class="pc-border" ng-style="highlight" style="background-color: rgb(43, 203, 237); background-image: linear-gradient(-'+unitsLeft[unit]+'deg, transparent 50%, rgb(43, 203, 237) 50%), linear-gradient(-'+unitsLeft[unit]+'deg, rgb(200, 224, 232) 50%, transparent 50%);"><div class="pc-circle" style="background-color: rgb(245, 251, 252);"><span class="pc-percent ng-binding">'+unitsLeft[unit]+'<p>'+unit+'</p></span></div></div></div>';

//returnString +='<div class="pc-container ng-isolate-scope" percent="'+percentminus+'"><div class="pc-border" ng-style="highlight" style="background-color: rgb(43, 203, 237); background-image: linear-gradient('+degrees+'deg, transparent 50%, rgb(43, 203, 237) 50%), linear-gradient(-'+degrees+'deg, rgb(200, 224, 232) 50%, transparent 50%);"><div class="pc-circle" style="background-color: rgb(245, 251, 252);"><span class="pc-percent ng-binding">'+percentminus+'%</span></div></div></div>';

returnString +=' <div class="col-xs-4"><div class="c100 p'+percent+' center"><span>'+unitsLeft[unit]+'</span><div class="slice"><div class="bar"></div><div class="fill"></div></div></div><small>'+unit+'</small></div>';
                               
                            }

}
                        }
                        return returnString;
                    }
                    function updateCountDown() {
                        element.html(displayString());
                    }

                    element.on('$destroy', function () {
                        $interval.cancel(countDownInterval);
                    });

                    countDownInterval = $interval(function () {
                        updateCountDown();
                    }, 1);
                }
            };
        });
}(angular));