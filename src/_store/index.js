import Vue from 'vue';
import Vuex from 'vuex';

import { alert } from './alert.module';
import { account } from './account.module';
import { users } from './users.module';
import { positions } from './positions.module';
import { positionsDetail } from './positionsDetail.module';
import { stocks } from './stocks.module';
import { crypto } from './crypto.module';

Vue.use(Vuex);

export const store = new Vuex.Store({
    modules: {
        alert,
        account,
        users,
        positions,
        positionsDetail,
        stocks,
        crypto
    }
});
