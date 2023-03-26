import React from 'react';

class Formulario extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            formulario: [],
        };
    }

    componentDidMount() {
        this.fetchFormulario();
    }

    async fetchFormulario() {
        const response = await fetch('xptoformulario'); // Replace with the API endpoint for fetching formulario data
        const data = await response.json();
        this.setState({ formulario: data });
    }

    render() {
        return (
            <table>
                <thead>
                    <tr>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Email</th>
                        <th>Phone Number</th>
                        <th>Subscribed to Newsletter</th>
                    </tr>
                </thead>
                <tbody>
                    {this.state.formulario.map(form => (
                        <tr key={form.id}>
                            <td>{form.firstName}</td>
                            <td>{form.lastName}</td>
                            <td>{form.email}</td>
                            <td>{form.phoneNumber}</td>
                            <td>{form.isSubscribed ? 'Yes' : 'No'}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        );
    }
}

export default Formulario;