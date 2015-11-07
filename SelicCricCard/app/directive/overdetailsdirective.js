app.directive('overview', function (RecursionHelper) {
    
    return {
        restrict: "EAC",
        scope: { OverWithDetails: '=' },
        template:
        '  <div class="score-overs" ng-repeat="(over,overDetails) in OverWithDetails">' +
        '    <div class="strike"><span>Over {{over}} ({{overDetails.length}} Ball)</span></div>' +
        '    <ul class="list-unstyled score-list">' +
        '        <li ng-repeat="item in overDetails">' +
        '            <a href="#/4728/5/6" class="clearfix"><span class="score-b">{{over}}.{{item.BallNumber}}</span><span class="score-c">{{item.Description}}</span><span class="score-r">{{item.RunTaken}} Run</span></a>' +
        '        </li>' +
        '    </ul>' +
        '</div>',
        compile: function (element) {
            
            return RecursionHelper.compile(element);
        }
    };
});