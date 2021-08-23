import {ThemeProvider} from 'styled-components';
import { theme } from './theme';
import {DefaultLayout} from './pages/_defaultLayout';
import {BrowserRouter} from 'react-router-dom';

const App = () => {
    return (
        <BrowserRouter>
            <ThemeProvider theme={theme}>
                <DefaultLayout />
            </ThemeProvider>
        </BrowserRouter>
    );
};

export default App;
