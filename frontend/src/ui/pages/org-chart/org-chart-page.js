import React, {useEffect, useRef, useState} from "react";
import {useParams} from "react-router-dom";
import OrgChart from "../../components/org-chart";
import {getOrgChartResults} from "../../../services/org-chart";
import './org-chart-page.css';
import ScrollContainer from 'react-indiana-drag-scroll'
import ZoomInIcon from "@material-ui/icons/ZoomIn";
import ZoomOutIcon from "@material-ui/icons/ZoomOut";
import {Button} from "@material-ui/core";
import { TransformWrapper, TransformComponent } from "react-zoom-pan-pinch";

const OrgChartPage = () => {
    const heading_text = "Organizational Chart";
    const [orgChartResults, setOrgChartResults] = useState([]);
    let { id } = useParams();
    useEffect(() => {
        async function getOrgData() {
            getOrgChartResults(id).then(res => {
                setOrgChartResults(res)
            })
        }
        getOrgData();
    }, [id]);

    const container = useRef(null);

    useEffect(() => {
        let pageWidth = document.getElementById('titleWrapper').offsetWidth
        container.current.getElement().scrollTo(2830-pageWidth/2, 400);
    }, []);

    return (
        <div className={"page-wrapper"}>
            <TransformWrapper
                defaultScale={0.8}
                defaultPositionX={0}
                defaultPositionY={100}
                zoomOut={{
                    step: 10,
                }}
                zoomIn={{
                    step: 10
                }}
                options={{
                    limitToBounds: false,
                    minScale: 0.3,
                    maxScale: 1.3
                }}
                wheel={{
                    disabled: true
                }}
                pan={{
                    disabled: true
                }}

            >

                {({ zoomIn, zoomOut, resetTransform, ...rest }) => (
                    <React.Fragment>
                        <div className="page-title-wrapper" id={"titleWrapper"}>
                            <div className="page-title-box">
                                <div className="page-title">
                                    <div className={"title"}> {heading_text} </div>
                                    <div className={"zoom-icons"} >
                                        <Button className={"icon-button"} onClick={zoomOut} ><ZoomOutIcon/></Button>
                                        <Button className={"icon-button"} onClick={zoomIn} ><ZoomInIcon/></Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <ScrollContainer className={"container"} ref={container}>
                        <TransformComponent >

                                <div className={"orgchart-wrapper"} id={"orgchart-wrapper"}>
                                    <OrgChart data={orgChartResults}/>
                                </div>
                        </TransformComponent>
                            </ScrollContainer>



                    </React.Fragment>
                )}
            </TransformWrapper>
            <div className={"page-bottom"}> </div>
        </div>
    );
};

export default OrgChartPage;
