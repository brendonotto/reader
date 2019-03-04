import './RssItem.css';
import React from 'react';

const RssItem = (props) => {
    return (
        <div className="rssContainer">
            <a className="rssItem" href={props.link}>{props.title}</a>
            {/* <br /> */}
            <span className="publishDate">{new Intl.DateTimeFormat('en-US', { year: 'numeric', month: 'long', day: '2-digit'}).format(new Date(props.publishDate))}</span>
        </div>
    );
}

export default RssItem;