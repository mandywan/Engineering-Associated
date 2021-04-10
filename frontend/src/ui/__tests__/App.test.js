import React from 'react';
import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom/extend-expect';
import { MemoryRouter, BrowserRouter } from 'react-router-dom'

import App from '../App';

const renderWithRouter = (ui, { route = '/' } = {}) => {
    window.history.pushState({}, 'Test page', route)
  
    return render(ui, { wrapper: BrowserRouter })
}  

describe("App", () => {
    it("renders App", () => {
        render(<App />, { wrapper: MemoryRouter })
        expect(screen.getByText(/Pinned Profiles/i)).toBeInTheDocument()
        expect(screen.getByText(/Resume Search/i)).toBeInTheDocument()
        expect(screen.getByText(/Explore/i)).toBeInTheDocument()
    });

    // it("conducts search: name 'ian'", () => {
    //     const route = '/search?q=%7B"meta"%3A%5B%5D%2C"Name"%3A%7B"type"%3A"OR"%2C"values"%3A%5B"ian"%5D%7D%7D'
    //     renderWithRouter(<SearchPage />, { route })
    //     expect(screen.getByText(/22 Search Result(s)/i)).toBeInTheDocument()

    // })
});
