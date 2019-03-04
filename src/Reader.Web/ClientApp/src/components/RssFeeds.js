import React from 'react';
import RssFeed from "./RssFeed";

const RssItems = props => (
    <div>
        {props.feedModels.map(feedModel => (
            <RssFeed key={feedModel.feedName} {...feedModel} />
        ))}
    </div>
)

export default RssItems;