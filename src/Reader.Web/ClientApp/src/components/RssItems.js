import React from 'react';
import RssItem from "./RssItem";

const RssItems = props => (
    <div>
        {props.items.map(item => (
            <RssItem {...item} />
        ))}
    </div>
)

export default RssItems;