package com.example.student.adprojectsa40team4;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.Fragment;
import android.content.DialogInterface;
import android.os.AsyncTask;
import android.os.Bundle;
import android.text.InputType;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

import com.example.student.adprojectsa40team4.dummy.DummyContent;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

/**
 * A fragment representing a list of Items.
 * <p/>
 * Large screen devices (such as tablets) are supported by replacing the ListView
 * with a GridView.
 * <p/>
 * Activities containing this fragment MUST implement the {@link OnFragmentInteractionListener}
 * interface.
 */
public class DisbursementListFragment extends Fragment implements AbsListView.OnItemClickListener {

    private OnFragmentInteractionListener mListener;

    private ListAdapter mAdapter;
    ListView list_data, list_heading;
    ArrayList<HashMap<String, String>> mylist, mylist_title;
    ListAdapter adapter_title, adapter;
    HashMap<String, String> map_heading, map_list;
    List<String> valuesList = new ArrayList<String>();
    String deptId,email,name;
    Integer updateResult;
    String txtpassword;
    LogInModel objLogin;
    TextView deplabel,depName,empLable,empName;
    TextView totalCount;

    // TODO: Rename and change types of parameters
    public static DisbursementListFragment newInstance(String param1, String param2) {
        DisbursementListFragment fragment = new DisbursementListFragment();
        Bundle args = new Bundle();
        fragment.setArguments(args);
        return fragment;
    }

    public DisbursementListFragment() {
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        // TODO: Change Adapter to display your content
        mAdapter = new ArrayAdapter<DummyContent.DummyItem>(getActivity(),
                android.R.layout.simple_list_item_1, android.R.id.text1, DummyContent.ITEMS);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.disbursementlistfragment, container, false);
        list_heading = (ListView) view.findViewById(R.id.list_heading);
        list_data = (ListView) view.findViewById(R.id.list_data);
        totalCount = (TextView)view.findViewById(R.id.totalCount);

       depName = (TextView) view.findViewById(R.id.depName);
        empName = (TextView) view.findViewById(R.id.empName);

        Bundle arg = getArguments();

        if (arg != null) {
            HashMap<String,String> item = (HashMap<String,String>) arg.getSerializable("item");
            if (item != null) {
                email = item.get("EmpEmail");
                deptId = item.get("DeptId");
                name = item.get("EmpRepName");
            }
        }
        depName.setText(deptId);
        empName.setText(name);

        PopulateList();
        list_data.setOnItemClickListener(this);

        final Button confirmbtn = (Button) view.findViewById(R.id.btnConfirm);
        confirmbtn.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {

                final AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
                builder.setTitle("Confirmation for Disbursement List");

                final EditText pwd = new EditText(getActivity());

                pwd.setInputType(InputType.TYPE_CLASS_TEXT | InputType.TYPE_TEXT_VARIATION_PASSWORD);
                builder.setMessage(email.toString());
                //builder.setMessage("Test");
                builder.setView(pwd);
                builder.setPositiveButton("OK", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {

                        txtpassword = pwd.getText().toString();
                        if (!txtpassword.isEmpty()) {
                            new AsyncTask<Void, Void, Integer>() {
                                @Override
                                protected Integer doInBackground(Void... params) {
                                    objLogin = LogInModel.GetAuthenticateUser(email, txtpassword);
                                    if (objLogin != null)
                                        return 1;
                                    else return 0;
                                }
                                @Override
                                protected void onPostExecute(final Integer result) {
                                    if (result > 0) {
                                        Toast.makeText(getActivity(), "Correct Password", Toast.LENGTH_SHORT).show();
                                        new AsyncTask<Void, Void, Integer>() {
                                            @Override
                                            protected Integer doInBackground(Void... params) {
                                                updateResult = DisbursementInfo.UpdateDisburse(deptId,String.valueOf(LogInModel.empID));
                                                return updateResult;
                                            }
                                            @Override
                                            protected void onPostExecute(Integer updateResult) {
                                                if (updateResult > 0) {
                                                    Toast.makeText(getActivity(), "Approve Successful", Toast.LENGTH_SHORT).show();
                                                    //btnSpeak.setEnabled(true);
                                                    confirmbtn.setEnabled(false);

                                                } else {
                                                    Toast.makeText(getActivity(), "Invalid Password", Toast.LENGTH_SHORT).show();
                                                    pwd.setText("");
                                                }
                                            }
                                        }.execute();

                                    } else {
                                        Toast.makeText(getActivity(), "Invalid Password", Toast.LENGTH_SHORT).show();
                                        pwd.setText("");
                                    }
                                }
                            }.execute();

                        } else {
                            Toast.makeText(getActivity(), "Type Password", Toast.LENGTH_SHORT).show();
                        }
                    }
                });
                builder.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        dialog.cancel();
                    }
                });
                builder.show();
            }
        });
        return view;
    }

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        try {
            mListener = (OnFragmentInteractionListener) activity;
        } catch (ClassCastException e) {
            throw new ClassCastException(activity.toString()
                    + " must implement OnFragmentInteractionListener");
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
        if (null != mListener) {
            // Notify the active callbacks interface (the activity, if the
            // fragment is attached to one) that an item has been selected.
            mListener.onFragmentInteraction(DummyContent.ITEMS.get(position).id);
        }
    }

    public void setEmptyText(CharSequence emptyText) {
        View emptyView = list_data.getEmptyView();

        if (emptyView instanceof TextView) {
            ((TextView) emptyView).setText(emptyText);
        }
    }

    public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        void onFragmentInteraction(String id);
    }

    public void PopulateList(){
        mylist = new ArrayList<HashMap<String, String>>();
        mylist_title = new ArrayList<HashMap<String, String>>();

        map_heading = new HashMap<String, String>();

        map_heading.put("description", "Item Desp");
        map_heading.put("requestedqty", "Req: Qty");
        map_heading.put("recievedqty", "Receive Qty");
        map_heading.put("outstandingqty", "Outst: Qty");
        map_heading.put("uom", "UOM");
        mylist_title.add(map_heading);

        adapter_title = new SimpleAdapter(getActivity(),
                mylist_title,
                R.layout.disburse_row,
                new String[] {"description", "requestedqty", "recievedqty", "outstandingqty","uom"},
                new int[]{R.id.col1, R.id.col2, R.id.col3, R.id.col4,R.id.col5});
        list_heading.setAdapter(adapter_title);

        new AsyncTask<Void, Void, ArrayList<DisbursementInfo>>() {
            @Override
            protected ArrayList<DisbursementInfo> doInBackground(Void... params) {
                ArrayList<DisbursementInfo> disburseList = DisbursementInfo.GetDisbursementInfoList(deptId);
                return disburseList;
            }

            @Override
            protected void onPostExecute(ArrayList<DisbursementInfo> disburseList) {
                if (disburseList.size() > 0) {
                    for (int i = 0; i < disburseList.size(); i++) {
                        DisbursementInfo df = disburseList.get(i);
                        map_list = new HashMap<String, String>();
                        map_list.put("description", String.valueOf(df.description));
                        map_list.put("requestedqty", String.valueOf(df.requestedqty));
                        map_list.put("recievedqty", String.valueOf(df.recievedqty));
                        map_list.put("outstandingqty", String.valueOf(df.outstandingqty));
                        map_list.put("uom", String.valueOf(df.UOM));
                        mylist.add(map_list);
                    }

                    adapter = new SimpleAdapter(getActivity(),
                            mylist,
                            R.layout.disburse_row,
                            new String[] {"description", "requestedqty", "recievedqty", "outstandingqty","uom"},
                            new int[]{R.id.col1, R.id.col2, R.id.col3, R.id.col4,R.id.col5});
                    list_data.setAdapter(adapter);
                    totalCount.setText("Total Items : " + String.valueOf(mylist.size()));

                } else {
                }
            }
        }.execute();
    }

}
