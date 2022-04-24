/*
Adam Bender
CST452 Mark Reha
2/6/22
Handles routing to all pages
*/

import Vue from 'vue';
import Router from 'vue-router';

import HomePage from '../home/HomePage'
import LoginPage from '../login/LoginPage'
import RegisterPage from '../register/RegisterPage'
import AddPositionPage from "../home/AddPositionPage";
import StockPage from "../home/StockPage";
import CryptoPage from "../home/CryptoPage";
import DetailPage from "../home/DetailPage";
import PositionHistoryPage from "../home/PositionHistoryPage";

Vue.use(Router);

export const router = new Router({
  mode: 'history',
  routes: [
    { path: '/', component: HomePage },
    { path: '/login', component: LoginPage },
    { path: '/register', component: RegisterPage },
    { path: '/addposition', component: AddPositionPage },
    { path: '/stocks', component: StockPage },
    { path: '/crypto', component: CryptoPage },
    { path: '/detail', component: DetailPage },
    { path: '/history', component: PositionHistoryPage },

    //otherwise redirect to home
    { path: '*', redirect: '/' }
  ]
});

router.beforeEach((to, from, next) => {
  //redirect to login page if not logged in and trying to access a restricted page
  const publicPages = ['/login', '/register'];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = localStorage.getItem('user');

  if (authRequired && !loggedIn) {
    return next('/login');
  }

  next();
})