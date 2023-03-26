import React, { Component } from 'react';

export class Formularios extends Component {
    static displayName = Formularios.name;

    constructor(props) {
        super(props);
        this.state = { formularios: [], loading: true };
    }

    componentDidMount() {
        this.populateFormularioData();
    }

    static renderFormulariosTable(formularios) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Email</th>
                        <th>Phone Number</th>
                    </tr>
                </thead>
                <tbody>
                    {formularios.map(formulario =>
                        <tr key={formulario.firstName}>
                            <td><a href={`/formularios-data/${formulario.id}`}>{formulario.firstName}</a></td>
                            <td>{formulario.lastName}</td>
                            <td>{formulario.email}</td>
                            <td>{formulario.phoneNumber}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Formularios.renderFormulariosTable(this.state.formularios);

        return (
            <div>
                <h1 id="tabelLabel" >Campanha XPTO</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async populateFormularioData() {
        const response = await fetch('xptoformulario');
        const data = await response.json();
        this.setState({ formularios: data, loading: false });
    }
}