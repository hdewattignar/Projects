var app3 = angular.module('app3', []);

app3.controller('gListCtrl', function ($scope) {
    
    $scope.groceries = [
        {item: "Tomatoes", purchased: false},
        {item: "Potatoes", purchased: false},
        {item: "Cabbage", purchased: false},
        {item: "Onions", purchased: false}];
});