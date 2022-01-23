import * as am5 from "@amcharts/amcharts5";
import * as am5map from "@amcharts/amcharts5/map"
import am5geodata_world from "@amcharts/amcharts5-geodata/worldHigh"

export function createMapChart(root: am5.Root) : am5map.MapChart
{    
    let map = root.container.children.push(
        am5map.MapChart.new(root, {
          panX: "none",
          panY: "none",
          wheelY: "none",
          projection: am5map.geoEqualEarth(),
        }));

    var polygonSeries = map.series.push(
        am5map.MapPolygonSeries.new(root, 
            {
                fill: am5.color(0x999999),
                geoJSON: am5geodata_world
    }));

    polygonSeries.mapPolygons.template.setAll({
        tooltipText: "{name}",
        toggleKey: "active",
        interactive: true
    });

    polygonSeries.mapPolygons.template.states.create("hover", 
    {
        fill: root.interfaceColors.get("primaryButtonHover")
    });

    
    map.set("zoomControl", am5map.ZoomControl.new(root, {}));
    return map;
}