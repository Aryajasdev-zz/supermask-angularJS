'use strict';
var myCart = angular.module('ngCart', ['ngCart.directives']);

myCart.config([function () { }]);
myCart.provider('$ngCart', function () {
	this.$get = function () { };
});

myCart.run(['$rootScope', 'ngCart','ngCartItem', 'store', function ($rootScope, ngCart, ngCartItem, store) {
	$rootScope.$on('ngCart:change', function(){
		ngCart.$save();
	});
	if (angular.isObject(store.get('cart'))) {
		ngCart.$restore(store.get('cart'));
	} else {
		ngCart.init();
	}
}]);
	
myCart.service('ngCart', ['$rootScope', 'ngCartItem', 'store', function ($rootScope, ngCartItem, store, $http) {
	this.init = function(){
		this.$cart = {
			postage : null,
			postageid : null,	
			discount : null,
			discode : null,	
			items : []
		};
	};
	
	this.getaddress = function () {
		console.log($rootScope.cardepcode);
	};
	
	this.addItem = function (id, name, price, quantity, data, url, img, sizeid, isstock) {
		var inCart = this.getItemById(id);
		if (typeof inCart === 'object'){
			//Update quantity of an item if it's already in the cart
			inCart.setQuantity(quantity, true);
			$rootScope.$broadcast('ngCart:itemUpdated', inCart);
		} else {
			var newItem = new ngCartItem(id, name, price, quantity, data, url, img, sizeid, isstock);
			this.$cart.items.push(newItem);
			$rootScope.$broadcast('ngCart:itemAdded', newItem);
		}
		$rootScope.$broadcast('ngCart:change', {});
	};
	this.getItemById = function (itemId) {
		var items = this.getCart().items;
		var build = false;
		angular.forEach(items, function (item) {
			if  (item.getId() === itemId) {
				build = item;
			}
		});
		return build;
	};
	
	this.setDiscount = function(discount){
		this.$cart.discount = discount;
		return this.getDiscount();
	};
	this.getDiscount = function(){
		if (this.getCart().items.length === 0) return 0;
		return  this.getCart().discount;
	};        
	
	this.setDiscode = function(discode){
		this.$cart.discode = discode;
		return this.getDiscode();
	};
	this.getDiscode = function(){
		if (this.getCart().items.length === 0) return 0;
		return  this.getCart().discode;
	};
	
	this.setPostage = function(postage){
		this.$cart.postage = postage;
		return this.getPostage();
	};
	this.getPostage = function(){
		if (this.getCart().items.length === 0) return 0;
		return  this.getCart().postage;
	};        
		
	this.setPostageid = function(postageid){
		this.$cart.postageid = postageid;			
		return this.getPostageid();
	};
	this.getPostageid = function(){
		if (this.getCart().items.length === 0) return 0;
		return  this.getCart().postageid;
	};   	

	this.setCart = function (cart) {
		this.$cart = cart;
		return this.getCart();
	};

	this.getCart = function(){
		return this.$cart;
	};

	this.getItems = function(){
		return this.getCart().items;
	};

	this.getTotalItems = function () {
		var count = 0;
		var items = this.getItems();
		angular.forEach(items, function (item) {
			count += item.getQuantity();
		});
		return count;
	};

	this.getTotalUniqueItems = function () {
		return this.getCart().items.length;
	};

	this.getSubTotal = function(){
		var total = 0;
		angular.forEach(this.getCart().items, function (item) {
			total += item.getTotal();
		});
		return +parseFloat(total).toFixed(2);
	};

	this.totalCost = function () {
		return +parseFloat(this.getSubTotal() + this.getPostage()).toFixed(2);
	};

	this.removeItem = function (index) {
		this.$cart.items.splice(index, 1);
		$rootScope.$broadcast('ngCart:itemRemoved', {});
	};
		
	this.ChangePostage = function (amt, pid) {
		this.$cart.postage = amt;
		this.$cart.postageid = pid;
	};
	
	this.removeItemById = function (id) {
		var cart = this.getCart();
		angular.forEach(cart.items, function (item, index) {
			if  (item.getId() === id) {
				cart.items.splice(index, 1);
			}
		});
		this.setCart(cart);
		$rootScope.$broadcast('ngCart:itemRemoved', {});
		$rootScope.$broadcast('ngCart:change', {});
	};

	this.empty = function () {            
		$rootScope.$broadcast('ngCart:change', {});
		this.$cart.items = [];
		localStorage.removeItem('cart');
	};

	this.toObject = function() {
		if (this.getItems().length === 0) return false;
		var items = [];
		angular.forEach(this.getItems(), function(item){
			items.push (item.toObject());
		});
		return {
			postage: this.getPostage(),
			postageid: this.getPostageid(),
			subTotal: this.getSubTotal(),
			totalCost: this.totalCost(),
			discount: this.getDiscount(),
			discode: this.getDiscode(),
			items: items
		};
	};

	this.$restore = function(storedCart){
		var _self = this;
		_self.init();
		_self.$cart.postage = storedCart.postage;
		_self.$cart.postageid = storedCart.postageid;
		angular.forEach(storedCart.items, function (item) {
			_self.$cart.items.push(new ngCartItem(item._id,  item._name, item._price, item._quantity, item._data, item._url, item._img, item._sizeid, item._isstock));
		});
		this.$save();
	};

	this.$save = function () {
		return store.set('cart', JSON.stringify(this.getCart()));
	};
}]);

myCart.factory('ngCartItem', ['$rootScope', '$log', function ($rootScope, $log) {
	var item = function (id, name, price, quantity, data, url, img, sizeid, isstock) {
		this.setId(id);
		this.setName(name);
		this.setPrice(price);
		this.setQuantity(quantity);
		this.setData(data);
		this.setUrl(url);
		this.setImg(img);
		this.setSizeid(sizeid);
		this.setIsStock(isstock);
	};

	item.prototype.setId = function(id){
		if (id)  this._id = id;
		else {
			$log.error('An ID must be provided');
		}
	};

	item.prototype.getId = function(){
		return this._id;
	};

	item.prototype.setName = function(name){
		if (name)  this._name = name;
		else {
			$log.error('A name must be provided');
		}
	};	
	item.prototype.getName = function(){
		return this._name;
	};		

	item.prototype.setPrice = function(price){
		var priceFloat = parseFloat(price);
		if (priceFloat) {
			if (priceFloat <= 0) {
				$log.error('A price must be over 0');
			} else {
				this._price = priceFloat;
			}
		} else {
			$log.error('A price must be provided');
		}
	};
	item.prototype.getPrice = function(){
		return parseFloat(this._price);
	};

	item.prototype.setQuantity = function(quantity, relative){
		var quantityInt = parseInt(quantity);
		if (quantityInt % 1 === 0){
			if (relative === true){
				this._quantity  += quantityInt;
				return this._quantity;
			} else {
				this._quantity = quantityInt;
				return this._quantity;
			}
			//if (this._quantity < 1) this._quantity = 1;
		} else {
			this._quantity = 1;
			$log.info('Quantity must be an integer and was defaulted to 1');
		}
	};
		
	item.prototype.getUrl = function(){
		return this._url;
	};
		
	item.prototype.setUrl = function(url){
		if (url) this._url = url;
	};
		
	item.prototype.setImg = function(img){
		if (img)  this._img = img;
		else {
			$log.error('A Image must be provided');
		}
	};
		
	item.prototype.getImg = function(){
		return this._img;
	};
		
	item.prototype.setSizeid = function(sizeid){
		if (sizeid)  this._sizeid = sizeid;
		else {
			$log.error('An SizeID must be provided');
		}
	};

	item.prototype.getSizeid = function(){
		return this._sizeid;
	};
	
	item.prototype.setIsStock = function(isstock){
		if (isstock)  this._isstock = isstock;
		else {
			$log.error('An is stock must be provided');
		}
	};

	item.prototype.getIsStock = function(){
		return this._isstock;
	};
		
	item.prototype.getQuantity = function(){
		return this._quantity;
	};

	item.prototype.setData = function(data){
		if (data) this._data = data;
	};

	item.prototype.getData = function(){
		if (this._data) return this._data;
		else $log.info('This item has no data');
	};

	item.prototype.getTotal = function(){
		return +parseFloat(this.getQuantity() * this.getPrice()).toFixed(2);
	};

	item.prototype.toObject = function() {
		return {
			id: this.getId(),
			name: this.getName(),
			price: this.getPrice(),
			quantity: this.getQuantity(),
			data: this.getData(),
			total: this.getTotal(),
			url: this.getUrl(),
			img: this.getImg(),
			sizeid: this.getSizeid(),
			isstock: this.getIsStock()
		};
	};
	return item;
}]);

myCart.service('store', ['$window', function ($window) {
	return {
		get: function (key) {
			if ($window.localStorage[key]) {
				var cart = angular.fromJson($window.localStorage[key]);
				return JSON.parse(cart);
			}
			return false;
		},
		set: function (key, val) {
			if (val === undefined) {
				$window.localStorage.removeItem(key);
			} else {
				$window.localStorage[key] = angular.toJson(val);
			}
			return $window.localStorage[key];
		}
	};
}]);

myCart.controller('CartController',['$scope', 'ngCart', '$http', function($scope, ngCart, $http) {
		$scope.ngCart = ngCart;	
		$scope.getaddress = function () {
			$scope.isadd = false;
			$http.get('https://api.getaddress.io/v2/uk/' + $scope.cardepcode + '?api-key=j1EEuFFyG0eiOuKDvtsDKA2553').then(function (response) {
				$scope.adds = response.data.Addresses;
				$scope.isadd = true;
			});
		};
		
		$scope.showaddress = function () {
			var addparts = $scope.pcodeadds.split(',');
			$scope.billing.cardHno = addparts[0];
			$scope.billing.cardAddress = addparts[1];
			$scope.billing.cardCity = addparts[5];
			$scope.billing.cardCounty = addparts[5];
			$scope.billing.cardCountry = "United Kingdom";
			$scope.billing.cardPostcode = $scope.cardepcode;
		};
}]).value('version', '0.0.3-rc.1');

'use strict';
var myCartd = angular.module('ngCart.directives', ['ngCart.fulfilment']);
myCartd.controller('CartController', ['$scope', 'ngCart', function ($scope, ngCart) {
	$scope.ngCart = ngCart;
}]);

 myCartd.directive('ngcartAddtocart', ['ngCart', function(ngCart){
	function addtocart(scope, element, attrs) {
		scope.attrs = attrs;
		scope.inCart = function(){
			return  ngCart.getItemById(attrs.id);
		};
		if (scope.inCart()){
			scope.q = ngCart.getItemById(attrs.id).getQuantity();
		} else {
			scope.q = parseInt(scope.quantity);
		}
		scope.qtyOpt =  [];
		for (var i = 1; i <= scope.quantityMax; i++) {
			scope.qtyOpt.push(i);
		}		
	}
 
	return {
		restrict : 'E',
		controller : 'CartController',
		scope: {
			id:'@',
			name:'@',
			quantity:'@',
			quantityMax:'@',
			price:'@',
			url:'@',
			img:'@',
			data:'=',
			sizeid:'@',
			isdetails:'@',
			isstock:'@'
		},
		transclude: true,
		templateUrl: 'template/addtocart.html',
		link:addtocart
	};
}]);

myCartd.directive('ngcartCart', [function(){
	return {
		restrict : 'E',
		controller : 'CartController',
		scope: {},
		templateUrl: 'template/cart.html',
		link:function(scope, element, attrs){}
	};
}]);

myCartd.directive('ngcartSummary', [function () {
	return {
		restrict: 'E',
		controller: 'CartController',
		scope: {},
		transclude: true,
		templateUrl: 'template/summary.html'
	};
}]);

myCartd.directive('ngcartCheckout', [function(){
	return {
		restrict : 'E',
		controller : ('CartController', ['$scope', 'ngCart', '$http', 'fulfilmentProvider', function($scope, ngCart, $http, fulfilmentProvider) {
			$scope.ngCart = ngCart;
			$http.get("/getPostage/postages?apikey=45724525-D866-4F0A-8F9A-4E352AAE15C1").then(function (response) {		
				$scope.postages = response.data;							
			});		
			
			$scope.billing={};
			$scope.delivery={};
			$scope.isdel = false;			
			$scope.checkdel = function() {
				$scope.isdel = !$scope.isdel;
				if($scope.isdel === true){
					$scope.delivery=$scope.billing;	
				}else{
					$scope.delivery={};
				}
			};
			
			$scope.addFormFields = function (form, data) {
				if (data !== null) {
					$.each(data, function (name, value) {
						if (value !== null) {
							var input = $("<input></input>").attr("type", "hidden").attr("name", name).val(value);
							form.append(input);
						}
					});
				}
			};
			
			$scope.checkout = function (type) {
				fulfilmentProvider.setService($scope.service);
				fulfilmentProvider.setSettings($scope.settings);
				fulfilmentProvider.setBilling($scope.billing);
				fulfilmentProvider.setDelivery($scope.delivery);
				var promise = fulfilmentProvider.checkout(type);
				promise.then(function (response) {
					var orderid = response.data;
					var form = $('<form/></form>');
					console.log(orderid);
					if (type === 'paypal') {
						var data = {
							cmd: "_xclick",
							business: 'arya2k7@gmail.com',
							upload: "1",
							rm: "2",
							charset: "utf-8",
							currency_code: "GBP",
							item_number: orderid,
							item_name: "Super Party Masks Order",
							amount: $scope.ngCart.getSubTotal(),
							shipping: $scope.ngCart.getPostage()
						};
						// build form			            
						form.attr("action", "https://www.sandbox.paypal.com/cgi-bin/webscr");
						form.attr("method", "POST");
						form.attr("style", "display:none;");
						$scope.addFormFields(form, data);
						$("body").append(form);
						console.log(data);
						// submit form
						form.submit();
						form.remove();
					} else {			            
						form.attr("action", "#/realpay/" + orderid);
						form.attr("method", "POST");
						form.attr("style", "display:none;");
						$("body").append(form);
						// submit form
						form.submit();
						form.remove();
					}
				});
			};
		}]),
		scope: {
			service:'@',
			settings:'=',	
			billing:'@',					
			delivery:'@'
		},
		transclude: true,
		templateUrl: 'template/checkout.html'
	};
}]);
	
var myCartf = angular.module('ngCart.fulfilment', []);

myCartf.service('fulfilmentProvider', ['$injector', function($injector){
	this._obj = {
		service : undefined,
		settings : undefined,
		billing: undefined,
		delivery: undefined
	};
	this.setService = function(service){
		this._obj.service = service;
	};
	this.setSettings = function(settings){
		this._obj.settings = settings;
	};
	this.setBilling = function(billing){
		this._obj.billing = billing;
	};
	this.setDelivery = function(delivery){
		this._obj.delivery = delivery;
	};	
	
	this.checkout = function (type) {
		var provider = $injector.get('ngCart.fulfilment.' + this._obj.service);
		return provider.checkout(this._obj.settings, this._obj.billing, this._obj.delivery, type);
	};
}]);

myCartf.service('ngCart.fulfilment.http', ['$http', 'ngCart', function($http, ngCart){
	this.checkout = function (settings, billing, delivery, type) {
		return $http.get(settings.url, {
			params: {
				itemdata: ngCart.toObject(),
				billdata: billing,
				deldata: delivery,
				type: type,
				siteid: 6
			}
		});
	};
}]);

myCartf.service('ngCart.fulfilment.paypal', ['$http', 'ngCart', function($http, ngCart){
	this.checkout = function (settings, billing, delivery, type) {
		return $http.get(settings.url, {
			params: {
				itemdata: ngCart.toObject(),
				billdata: billing,
				deldata: delivery,
				type: type,
				siteid: 6
			}
		});
	};
}]);



