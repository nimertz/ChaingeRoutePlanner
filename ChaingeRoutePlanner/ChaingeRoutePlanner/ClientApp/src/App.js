import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Map } from './components/Map';
import { OrderPage } from './components/OrderPage';
import { BikePage } from './components/BikePage';
import { Counter } from './components/Counter';
import { ListPage } from './components/ListPage';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' componeot={Counter} />
        <Route path='/fetch-data' component={FetchData} />
        <Route path='/map' component={Map} />
        <Route path='/orderPage' component={OrderPage} />
        <Route path='/bikePage' component={BikePage} />
        <Route path='/listPage' component={ListPage} />
      </Layout>
    );
  }
}
