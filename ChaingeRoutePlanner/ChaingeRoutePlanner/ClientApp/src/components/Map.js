import React, { Component, useState } from 'react';
import { MapContainer, TileLayer, Marker, Popup, useMapEvents, Polyline as PL } from 'react-leaflet'
import Polyline from '@mapbox/polyline'

function LocationMarker(props) {
    const [position, setPosition] = useState(null)
    const map = useMapEvents({
        click(e) {
            console.log('Click', e)
        },
        moveend() {
            let center = map.getCenter();
            let zoom = map.getZoom();
            console.log('Zoom:', zoom, 'Center:', center)
        },
        locationfound(e) {
            setPosition(e.latlng)
            map.flyTo(e.latlng, map.getZoom())
        },
    })

    return position === null ? null : (
        <Marker position={position}>
            <Popup>You are here</Popup>
        </Marker>
    )
}

export class Map extends Component {
    static displayName = Map.name;


    static decodeGeometry(geometry) {
        return Polyline.decode(geometry);
    }

    static generateRandomPolygonColor() {
        let colors = ["red", "blue", "green", "purple", "brown"];
        return colors[Math.floor(Math.random() * colors.length)];
    }

    static convertDuration(duration) {
        //convert to hh:mm:ss
        let hours = Math.floor(duration / 3600);
        let minutes = Math.floor((duration - (hours * 3600)) / 60);
        let seconds = duration - (hours * 3600) - (minutes * 60);
        return `${hours}h ${minutes}m ${seconds}s`;
    }

  render () {
      return (
          <MapContainer center={this.props.center} zoom={15} scrollWheelZoom={true} style={{ height: this.props.height }} eventHandlers={{
            click: () => {
                console.log('map clicked')
            },
        }}>
            <TileLayer
                attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
                url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
            />
            {this.props.routes.map(route =>
                <PL positions={Map.decodeGeometry(route.geometry)} color={Map.generateRandomPolygonColor()} />
            )}
            {this.props.routes.map(route =>
                route.steps.map(step =>
                    <Marker position={[step.location[1], step.location[0]]}>
                        <Popup>
                            {step.description}<br />
                            <b>Vehicle:</b> {route.vehicle}<br />
                            <b>Type:</b> {step.type}<br />
                            <b>Travel time:</b> {Map.convertDuration(step.duration)}<br />
                            <b>Vehicle Load:</b> {step.load}<br />
                        </Popup>
                    </Marker>
                ))}
            <LocationMarker effectOn={this} />
        </MapContainer>
    );
  }
}