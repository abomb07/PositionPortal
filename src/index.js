import Vue from 'vue';
import VeeValidate from 'vee-validate';
import BootstrapVue from "bootstrap-vue";
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";

import { store } from './_store';
import { router } from './_helpers';
import App from './app/App';

Vue.use(BootstrapVue);
Vue.use(VeeValidate);

new Vue({
    el: '#app',
    router,
    store,
    render: h => h(App)
});