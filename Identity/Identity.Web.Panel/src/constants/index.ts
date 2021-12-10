export const bottomItems = [
    {
        id: 0,
        title: 'Home',
        icon: 'mdi-home',
        to: '/'
    }, {
        id: 1,
        title: 'Applications',
        icon: 'mdi-application',
        to: '/application'
    }, {
        id: 2,
        title: 'Profile',
        icon: 'mdi-account',
        to: '/profile'
    }
]

export const navigationItems = [
    {
        id: 0,
        title: 'Home',
        icon: 'mdi-home',
        to: '/'
    },
    {
        id: 1,
        title: 'Applications',
        icon: 'mdi-application',
        to: '/application'
    },
    {
        id: 2,
        title: 'Profile',
        icon: 'mdi-account',
        to: '/profile'
    }
]

export const messages = {
    netWorkError: (message: string) => ({
        status: false,
        code: 500,
        title: 'Connection to server faild',
        message: message
    })
}

export const applicationTableHeaders = [
    {
        text: 'Image',
        align: 'start',
        sortable: false,
        value: 'image',
    },
    {
        text: 'Name',
        align: 'start',
        sortable: true,
        value: 'name'
    },
    {
        text: 'Api Key',
        align: 'start',
        sortable: true,
        value: 'apiKey'
    },
    {
        text: 'Actions',
        align: 'start',
        sortable: false,
    }
]