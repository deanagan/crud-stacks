import React from 'react';
import { Switch, Route } from 'react-router-dom';
import { Auth, Contact, Home } from '../pages';
import {RoutePaths} from './paths';


export const Routes = () => (
    <Switch>
        <Route exact path={RoutePaths.home} component={Home} />
        <Route exact path={RoutePaths.contact} component={Contact} />
        <Route exact path={RoutePaths.auth} component={Auth} />
    </Switch>
);
