import * as am5 from "@amcharts/amcharts5";
import * as am5map from "@amcharts/amcharts5/map"
import am5geodata_world from "@amcharts/amcharts5-geodata/worldHigh"

//models
import { CountryDataGroupByDate } from "../models/CountryDataGroupByDate"

export function createMapChart(root: am5.Root, countryData: CountryDataGroupByDate) : am5map.MapChart
{    
    let map = root.container.children.push(
        am5map.MapChart.new(root, {
          panX: "rotateX",
          panY: "translateY",
          wheelY: "zoom",
          projection: am5map.geoEqualEarth(),
          layout: root.horizontalLayout,
        }));

    var polygonSeries = map.series.push(
        am5map.MapPolygonSeries.new(root, 
            {
                fill: am5.color(0x999999),
                geoJSON: am5geodata_world,
                valueField: "value"              
    }));

    polygonSeries.mapPolygons.template.setAll({
        tooltipText: "{name}: {value}"
      });

    polygonSeries.mapPolygons.template.states.create("hover", 
    {
        fill: root.interfaceColors.get("primaryButtonHover")
    });

    polygonSeries.set("heatRules", [{
        target: polygonSeries.mapPolygons.template,
        dataField: "value",
        min: am5.color(0xff621f),
        max: am5.color(0x661f00),
        key: "fill",
    }]);

    polygonSeries.mapPolygons.template.events.on("pointerover", function(ev) {
        // var dataValue = ev.target.dataItem?.get("value") as number;
        // if(dataValue === undefined)
        //     dataValue = 0;
        // heatLegend.showValue(dataValue);
    });  

    var heatLegend = map.children.push(am5.HeatLegend.new(root, {
        orientation: "vertical",
        startColor: am5.color(0xff621f),
        endColor: am5.color(0x661f00),
        startText: "Lowest",
        endText: "Highest",
        stepCount: 5,
        height: am5.p50,       
      }));
      
      heatLegend.startLabel.setAll({
        fontSize: 12,
        fill: heatLegend.get("startColor")
      });
      
      heatLegend.endLabel.setAll({
        fontSize: 12,
        fill: heatLegend.get("endColor")
      });

    
    map.set("zoomControl", am5map.ZoomControl.new(root, {}));
    return map;
}