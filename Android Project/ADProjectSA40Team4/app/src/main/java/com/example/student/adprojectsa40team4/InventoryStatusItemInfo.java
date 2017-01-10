package com.example.student.adprojectsa40team4;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;

/**
 * Created by student on 15/9/15.
 */
public class InventoryStatusItemInfo {
    public static String wcfUrl = ConfigurationManager.wcfUrl;
    public  String CategoryName,Description,ItemID,EmployeeEmail,UnitOfMeasurement;
    public  int Balance,ReorderLevel,ReorderQuantity,SuggestedQuantity;

    public InventoryStatusItemInfo(int Balance, String CategoryName, String Description, String ItemID, int ReorderLevel, int ReorderQuantity, int SuggestedQuantity, String UnitOfMeasurement)
    {
        this.Balance = Balance;
        this.CategoryName = CategoryName;
        this.Description = Description;
        this.ItemID = ItemID;
        this.ReorderLevel = ReorderLevel;
        this.ReorderQuantity= ReorderQuantity;
        this.SuggestedQuantity = SuggestedQuantity;
        this.UnitOfMeasurement = UnitOfMeasurement;
    }

    public static ArrayList<InventoryStatusItemInfo> DisplayLowLevelStock()
    {
        ArrayList<InventoryStatusItemInfo> arrList = new ArrayList<InventoryStatusItemInfo>();
        try
        {
            JSONArray jsonArray = JSONParser.getJSONArrayFromUrl(String.format("%s/DisplayLowLevelStock",wcfUrl));
            for (int i =0 ; i < jsonArray.length() ; i++)
            {
                JSONObject obj= jsonArray.getJSONObject(i);
                arrList.add(new InventoryStatusItemInfo(obj.getInt("Balance"),obj.getString("CategoryName"),obj.getString("Description"),obj.getString("ItemID"), obj.getInt("ReorderLevel"),obj.getInt("ReorderQuantity"),obj.getInt("SuggestedQuantity"),obj.getString("UnitOfMeasurement")));
            }
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
        return  arrList;
    }
}
