import React from 'react';
import RssItems from "./RssItems";

const RssFeed = props => {
    return (
        <div>
            <h4>{props.feedName}</h4>
            <RssItems items={props.rssItems} />
        </div>
    )
}

export default RssFeed;