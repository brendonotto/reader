import React, { Component } from 'react';
import RssFeeds from './RssFeeds';

export class Home extends Component {
  constructor(props) {
    super(props);
    this.state = { feedModels: [], loading: true };

    fetch('api/Rss/GetRssItems')
    .then(response => response.json())
    .then(data => {
      this.setState({ feedModels: data, loading: false});
    })
  }

  static renderRssItems (feedModels) {
    return (
      <div>
        <RssFeeds feedModels={feedModels} />
      </div>
    )
  }

  render () {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : Home.renderRssItems(this.state.feedModels);

    return (
      <div>
        <h3>Recent Articles</h3>
        {contents}
      </div>
    )
  }
}
