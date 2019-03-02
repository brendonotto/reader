import React from 'react';

const RssItem = (props) => {
    return (
        <div>
            <div>
                <a href={props.link}>{props.title}</a>
                <div>
                    {new Intl.DateTimeFormat('en-US', {
                        year: 'numeric',
                        month: 'long',
                        day: '2-digit'
                    }).format(new Date(props.publishDate))}
                </div>
            </div>
        </div>
    );
}

export default RssItem;