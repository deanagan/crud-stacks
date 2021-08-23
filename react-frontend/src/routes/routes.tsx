import React from 'react';
import { Switch, Route } from 'react-router-dom';
import { Contact, Home } from '../pages';
import {RoutePaths} from './paths';


export const Routes = () => (
    <Switch>
        <Route exact path={RoutePaths.home} component={Home} />
        <Route exact path={RoutePaths.contact} component={Contact} />
    </Switch>
);
