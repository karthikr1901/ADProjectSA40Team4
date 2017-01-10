package com.example.student.adprojectsa40team4;

import java.io.Serializable;
import java.util.HashMap;

/**
 * Created by student on 11/9/15.
 */
public class DisbursementListQueryItem extends HashMap<String, String> implements Serializable {
    public String deptID,empRepName,empEmail;

    public DisbursementListQueryItem(String deptID,String empRepName,String empEmail) {
        put("DeptId",deptID);
        put("EmpRepName",empRepName);
        put("EmpEmail",empEmail);
    }
}
