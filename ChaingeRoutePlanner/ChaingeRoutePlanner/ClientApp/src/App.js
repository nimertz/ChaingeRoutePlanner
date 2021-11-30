import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { OrderPage } from './components/OrderPage';
import { BikePage } from './components/BikePage';
import { Counter } from './components/Counter';
import { ListPage } from './components/ListPage';
import { OldRoutesPage } from './components/OldRoutesPage';
import { Login } from './components/Login';

import './custom.css'

export default class App extends Component {
    static displayName = App.name;
    static changieColor = (105, 219, 26);


  render () {
    return (
      <Layout>
        <Route path='/home' component={Home} />
        <Route path='/counter' componeot={Counter} />
        <Route path='/fetch-data' component={FetchData} />
        <Route path='/orderPage' component={OrderPage} />
        <Route path='/bikePage' component={BikePage} />
        <Route path='/listPage' component={ListPage} />
            <Route path='/oldroute' component={OldRoutesPage} />
            <Route exact path='/' component={Login} />
      </Layout>
    );
  }
}
