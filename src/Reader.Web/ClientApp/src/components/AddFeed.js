import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';

export class AddFeed extends Component {
    constructor(props) {
        super(props);

        this.state = {
            toHome: false,
            addFeedModel: {
                name: "",
                feedUrl: ""
            },
            
        };

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleInput = this.handleInput.bind(this);
    }

    handleSubmit(e) {
        e.preventDefault();
        let addModel = this.state.addFeedModel;

        fetch("api/Rss/AddFeed", {
            method: "POST",
            body: JSON.stringify(addModel),
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json"
            }
        }).then(() => this.setState(() => ({
                toHome: true
        })))
    }

    handleInput(e) {
        let value = e.target.value;
        let name = e.target.name;

        this.setState(
            prevState => ({
                addFeedModel: {
                    ...prevState.addFeedModel,
                    [name]: value
                },
                toHome: false
            })
        );
    }

    render() {
        if (this.state.toHome === true) {
            return <Redirect to="/" />
        }

        return(
            <div>
                <form className="container-fluid" onSubmit={this.handleSubmit}>
                    <label>
                        Feed name:
                    </label>
                    <input type="text" name={"name"} onChange={this.handleInput} value={this.state.addFeedModel.name} />
                    <label>
                        Feed Url:
                    </label>
                    <input type="text" name={"feedUrl"} onChange={this.handleInput} value={this.state.addFeedModel.feedUrl} />
                    <button style={{margin: "10px"}} className="btn btn-primary" onClick={this.handleSubmit}>Submit</button>
                </form>
            </div>
        )
    }
}