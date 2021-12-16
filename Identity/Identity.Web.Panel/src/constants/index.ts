import { Application } from "@/api/models/account.model"

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
        to: '/account/profile'
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
        to: '/account/profile'
    },

    {
        id: 3,
        title: 'Docs',
        icon: 'mdi-newspaper-variant-outline',
        to: 'docs'
    }
]

export const messages = {
    netWorkError: (message: string) => ({
        status: false,
        code: 500,
        title: 'Connection to server faild',
        message: message
    }),
    invalidForm: 'Please fill in all fields'
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
        value: 'actions'
    }
]

export const rules = {
    require: (value: string) => !!value || 'Required.',
    password: (value: string) => !!value && (value.length > 5 || 'Password required more than 6 characters')
}

export const app: Application = {
    apikey: "54AD86E7-BC7B-4B24-A43A-4AD0ADD6EBAF",
    password: "1G14ijWA"
}

export const userTableHeaders = [
    {
        text: 'User Name',
        align: 'start',
        sortable: true,
        value: 'userName',
    },
    {
        text: 'Full Name',
        align: 'start',
        sortable: true,
        value: 'fullName',
    }, {
        text: 'E-main',
        align: 'start',
        sortable: true,
        value: 'email',
    }, {
        text: 'Mobile No',
        align: 'start',
        sortable: true,
        value: 'mobileNo',
    },
]