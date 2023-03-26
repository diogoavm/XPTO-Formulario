import React, { Component } from "react";
import "../style/CreateFormulario.css";
import { createBrowserHistory } from "history";

class CreateFormulario extends Component {
    constructor(props) {
        super(props);

        this.state = {
            formulario: {
                FirstName: "",
                LastName: "",
                Email: "",
                PhoneNumber: "",
                IsSubscribedToNewsletter: false,
            },
            errors: {
                FirstName: "",
                LastName: "",
                Email: "",
                PhoneNumber: "",
            },
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.validateField = this.validateField.bind(this);
    }

    handleInputChange(event) {
        const { name, value, type, checked } = event.target;

        // Update the form data
        this.setState(
            (prevState) => ({
                formulario: {
                    ...prevState.formulario,
                    [name]: type === "checkbox" ? checked : value,
                },
            }),
            // After updating the form data, validate the field
            () => {
                this.validateField(name, value);
            }
        );
    }

    handleSubmit(event) {
        event.preventDefault();

        // If the form is invalid, don't submit it
        if (!this.isFormValid()) {
            return;
        }

        const formulario = {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(this.state.formulario),
        };

        fetch("xptoformulario", formulario)
            .then((response) => response.json())
            .then((data) => {
                console.log(data);
                // Do something with the response
                const history = createBrowserHistory();
                history.push("/");
            })
            .catch((error) => {
                console.log(error);
                // Do something with the error
            });
    }

    // Validate a field and update the errors object
    validateField(name, value) {
        const { errors } = this.state;

        switch (name) {
            case "FirstName":
                errors.FirstName = value.length < 2 ? "First name is too short" : "";
                break;
            case "LastName":
                errors.LastName = value.length < 2 ? "Last name is too short" : "";
                break;
            case "Email":
                errors.Email = !/\S+@\S+\.\S+/.test(value)
                    ? "Please enter a valid email address"
                    : "";
                break;
            case "PhoneNumber":
                errors.PhoneNumber = !/^\d{9}$/.test(value)
                    ? "Please enter a valid phone number"
                    : "";
                break;
            default:
                break;
        }

        // Update the errors object
        this.setState({ errors });
    }

    // Check if the form is valid
    isFormValid() {
        const { formulario, errors } = this.state;
        let isValid = true;

        // Check each field for errors
        Object.values(formulario).forEach((value, index) => {
            const name = Object.keys(formulario)[index];
            this.validateField(name, value);

            if (errors[name].length > 0) {
                isValid = false;
            }
        });

        return isValid;
    }

    render() {
        const { formulario, errors } = this.state;

        return (
            <form onSubmit={this.handleSubmit}>
                <div class="create-form-outer-div">
                    <div class="create-form-field-div">
                        <label htmlFor="firstName">First Name</label>
                        <input
                            type="text"
                            name="FirstName"
                            value={formulario.FirstName}
                            onChange={this.handleInputChange}
                            className={errors.FirstName ? "invalid" : ""}
                        />
                        {errors.FirstName && <span className="error">{errors.FirstName}</span>}
                    </div>
                    <div class="create-form-field-div">
                        <label htmlFor="lastName">Last Name</label>
                        <input
                            type="text"
                            name="LastName"
                            value={formulario.LastName}
                            onChange={this.handleInputChange}
                            className={errors.LastName ? "invalid" : ""}
                        />
                        {errors.LastName && <span className="error">{errors.LastName}</span>}
                    </div>
                    <div class="create-form-field-div">
                        <label htmlFor="email">Email</label>
                        <input
                            type="email"
                            name="Email"
                            value={formulario.Email}
                            onChange={this.handleInputChange}
                            className={errors.Email ? "invalid" : ""}
                        />
                        {errors.Email && <span className="error">{errors.Email}</span>}
                    </div>
                    <div class="create-form-field-div">
                        <label htmlFor="phoneNumber">Phone Number</label>
                        <input
                            type="tel"
                            name="PhoneNumber"
                            value={formulario.PhoneNumber}
                            onChange={this.handleInputChange}
                            className={errors.PhoneNumber ? "invalid" : ""}
                        />
                        {errors.PhoneNumber && <span className="error">{errors.PhoneNumber}</span>}
                    </div>
                    <div class="create-form-field-div-checkbox">
                        <label htmlFor="issubscribedtonewsletter">Subscribe to newsletter</label>
                        <input
                            type="checkbox"
                            name="IsSubscribedToNewsletter"
                            checked={formulario.IsSubscribedToNewsletter}
                            onChange={this.handleInputChange}
                        />
                    </div>
                    <button class="create-form-submit-button" type="submit">Submit</button>
                </div>
            </form>
        );
    }
}

export default CreateFormulario;