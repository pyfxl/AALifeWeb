//常量
$.extend($.const, {
    default: {
        bgimg: "/images/bg/bg.jpg",
        userimg: "/images/user/user.gif",
        pagebutton: 10,
        pagenumber: 30,
        pagenumbers: [10, 30, 50, 100]
    },
    webapi: {
        items: "/api/v1/itemsapi",
        itemnames: "/api/v1/itemnamesapi/{0}",
        users: "/api/v1/usersapi",
        userselects: "/api/v1/userselectsapi",
        usernames: "/api/v1/usernamesapi",
        roles: "/api/v1/rolesapi",
        permissions: "/api/v1/permissionsapi",
        permissionsupdate: "/api/v1/permissionsupdateapi",
        parameters: "/api/v1/parametersapi",
        parameters_id: "/api/v1/parametersapi/{0}",
        paramsbyname: "/api/v1/paramsbynameapi/{0}",
        userroles: "/api/v1/userrolesapi/{0}",
        userroleselects: "/api/v1/userroleselectsapi/{0}",
        categorytypes: "/api/v1/categorytypesapi/{0}",
        zhuantis: "/api/v1/zhuantisapi/{0}",
        zhuanzhangs: "/api/v1/zhuanzhangsapi/{0}",
        cards: "/api/v1/cardsapi/{0}",
        messagetemplates: "/api/v1/messagetemplatesapi"
    },
    pages: {
        category: "/User/CategoryPage",
        card: "/User/CardPage",
        zhuanti: "/User/ZhuanTiPage",
        zhuanzhang: "/User/ZhuanZhangPage"
    }
});
