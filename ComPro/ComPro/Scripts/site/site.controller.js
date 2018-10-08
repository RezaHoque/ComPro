(function () {
    var compro = angular.module('comproSite', []);

    compro.controller('homeController', ['$scope', '$http', function ($s, $h) {
        $s.showMoreEvents = false;
        $s.loadNotices = function () {
            $s.loadingNotice = true;
            var noticeUrl = "/Home/LatestNotice";

            $h({
                url: noticeUrl,
                method: "GET",
                async: true

            }).then(function (response) {
                $s.loadingNotice = false;
                $s.latestNotices = response.data;

            });
        };
        $s.loadEvents = function () {
            $s.loadingEvents = true;
            var eventUrl = "/Home/LatestEvent";
            $h({
                url: eventUrl,
                method: "GET",
                async: true

            }).then(function (response) {
                $s.loadingEvents = false;
                $s.latestEvents = response.data;

                if ($s.latestEvents.length > 0) {
                    $s.showMoreEvents = true;
                };

            });
        };
        $s.loadEvents();
        $s.loadNotices();
    }]);
})();