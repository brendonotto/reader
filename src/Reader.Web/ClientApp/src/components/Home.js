import React, { Component } from 'react';
import RssItems from './RssItems';

export class Home extends Component {
  constructor(props) {
    super(props);
    this.state = { items: [], loading: true };

    fetch('api/Rss/GetRssItems')
    .then(response => response.json())
    .then(data => {
      this.setState({ items: data, loading: false});
    })
  }

  static renderRssItems (items) {
    return (
      <div>
        <RssItems items={items} />
      </div>
    )
  }

  render () {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : Home.renderRssItems(this.state.items);

    return (
      <div>
        <h1>Rss Items</h1>
        {contents}
      </div>
    )
  }
}
