var PageConfig =
{
    ApiRoot: "http://localhost:8080/api/"
};

// Define the application
var AngularApp = angular.module('AngularApp', ['ngSanitize']);

// Define our controller
AngularApp.controller('LightsViewController', 
    function ($scope, $http) 
    {
        //initial declarations
        $scope.description = "Description goes here..";
        $scope.lightsModel = 
        {
            Request:
            {
                NumberOfPeople: 10,
                NumberOfLights: 10
            },
            Response: {},
            Steps: {},
            StepsVisible: false,
            HasResponse: false,
            NumberLightsOn: 0
        };
        $scope.isDebug = false;

        //define methods
        $scope.getResults = function()
        {
            return $http.post(PageConfig.ApiRoot + "lights", $scope.lightsModel.Request)
                .then(function (resp)
                {
                    $scope.lightsModel.Response = resp.data;
                    $scope.lightsModel.HasResponse = true;
                    $scope.lightsModel.NumberLightsOn = GetLightsOn($scope.lightsModel.Response);
                    $scope.lightsModel.StepsVisible = false;
                });
        };

        $scope.toggleSessionVisible = function(sessionId)
        {
            if (!$scope.lightsModel.StepsVisible)
            {
                return $http.get(PageConfig.ApiRoot + "lights/" + sessionId)
                    .then(function (resp)
                    {
                        $scope.lightsModel.Steps = resp.data.Steps;
                        $scope.lightsModel.StepsVisible = true;
                    });
            }
            else
            {
                $scope.lightsModel.StepsVisible = false;
            }
        };

        function GetLightsOn(response)
        {
            console.debug("GetLightsOn:start");
            if (!response) return 0;
            
            if (!response.Results) return 0;
            if (response.Results.length <= 0) return 0;
            
            var filtered = response.Results.filter(function (val) { return val; });
            console.debug("GetLightsOn:filtered[ok]", filtered);

            return filtered.length;
        }

        $scope.toggleDebug = function() 
        {
            $scope.isDebug = !$scope.isDebug;
        };

        angular.element(document).ready(function ()
        {
            return $http.get(PageConfig.ApiRoot + "lights")
                .then(function (resp)
                {
                    $scope.description = resp.data;
                });
        });
    });