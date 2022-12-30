
$(function () {
    $("#demoTree").tree({
        ui: {
            theme_name: "checkbox"
        },
        data: {
            type: "json",
            opts: {
                static: [
                        {
                            data: "Origination",
                            children: [
                                { data: "New Connection" },
                                { data: "Disconnection" },
                                { data: "Load Change" },
                                { data: "Corporate" },
                            ]
                        },
                        {
                            data: "Confirm Application"
                        }
                    ]
            }
        },
        plugins: {
            checkbox: {}
        }
    });
});