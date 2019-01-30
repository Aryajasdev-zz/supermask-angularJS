var myCacheService = function ($cacheFactory) {
    return $cacheFactory('myData');
};

myCacheService.$inject = ['$cacheFactory'];
