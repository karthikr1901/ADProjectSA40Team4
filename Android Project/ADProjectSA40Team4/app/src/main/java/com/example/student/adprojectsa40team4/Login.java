package com.example.student.adprojectsa40team4;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class Login extends Activity {
    EditText userid, password;
    String  userId , txtpassword;
    Intent intent;
    public static String USER = "";
    LogInModel objLogin;
    EditText pass;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        final SharedPreferences preferences= PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
        final SharedPreferences.Editor editor =preferences.edit();
        EditText username = (EditText)findViewById(R.id.et_userid);
         pass= (EditText)findViewById(R.id.et_password);
        username.setText(preferences.getString("USERNAME",""));
        pass.setText("");

        Button invokeLogin = (Button) findViewById(R.id.btn_login);
        invokeLogin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                userid = (EditText) findViewById(R.id.et_userid);
                USER = userid.getText().toString();
                password = (EditText) findViewById(R.id.et_password);
                txtpassword = password.getText().toString();
                if (!USER.isEmpty() && !txtpassword.isEmpty()) {
                    new AsyncTask<Void, Void, Integer>() {
                        @Override
                        protected Integer doInBackground(Void... params) {
                            objLogin = LogInModel.GetAuthenticateUser(USER,txtpassword);
                            if(objLogin == null)
                                return  0;
                            else
                            {
                                return  LogInModel.empRole;
                            }
                        }
                        @Override
                        protected void onPostExecute(Integer result) {
                            if (result > 0) {
                                editor.putString("USERNAME", USER);
                               // editor.putString("PASSWORD", password.getText().toString());
                                editor.commit();
                                intent = new Intent(Login.this, MainActivity.class);
                                startActivity(intent);

                            } else {
                                Toast.makeText(Login.this, getString(R.string.noaccount), Toast.LENGTH_SHORT).show();
                                pass.setText("");
                            }
                        }
                    }.execute();
                    pass.setText("");

                } else {
                    Toast.makeText(Login.this, getString(R.string.validdata), Toast.LENGTH_SHORT).show();
                    pass.setText("");
                }
            }
        });
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.menu_login, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            intent = new Intent(this, MainActivity.class);
            startActivity(intent);
        }

        return super.onOptionsItemSelected(item);
    }

}
