<?php
/*
Plugin Name: No Script Google Analytics
Plugin URI: http://remysharp.com
Description: Includes a Google analytics tracker for users without JavaScript enabled
Version: 0.1
Author: Remy Sharp
Author URI: http://remysharp.com

    Copyright 2009  Remy Sharp  (email : remy at remysharp dot com)

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation; either version 2 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/

function google_noscript_panel() {
  if (isset($_POST['save_no_script_settings'])) {
    $option_noscript_google_ua = $_POST['noscript_google_ua'];
    update_option('noscript_google_ua', $option_noscript_google_ua);
    ?>
<div class="updated">
  <p>No Script analytics settings saved</p>
</div>
<?php
  }
?>
<div class="wrap">
  <h2>Google No Script Google Analytic Settings</h2>

  <form method="post">
    <table class="form-table">
      <tr valign="top">
        <th scope="row">Analytics Account ID</th>
        <td>
          <input name="noscript_google_ua" type="text" id="flickr_id" value=""<?php echo get_option('noscript_google_ua'); ?>" size="20" />
        </td>
      </tr>
    </table>
    <div class="submit">
      <input type="submit" name="save_no_script_settings" value=""<?php _e('Save Settings', 'save_no_script_settings') ?>" />
    </div>
  </form>
</div>
<?php
}

function google_noscript() {
  
  $var_utmac = get_option('noscript_google_ua');
  
  $var_utmhn = get_bloginfo('url'); //enter your domain
  $var_utmn = rand(1000000000,9999999999); //random request number
  $var_cookie = rand(10000000,99999999); //random cookie number
  $var_random = rand(1000000000,2147483647); //number under 2147483647
  $var_today = time(); //today
  $var_referer = @$_SERVER['HTTP_REFERER']; //referer url

  $var_uservar = '-'; //enter your own user defined variable
  $var_utmp = '/noscript'; //this example adds a fake file request to the (fake) tracker directory (the image/pdf filename).

 $urchinUrl = 'http://www.google-analytics.com/__utm.gif?utmwv=1&utmn='.$var_utmn.'&utmsr=-&utmsc=-&utmul=-&utmje=0&utmfl=-&utmdt=-&utmhn='.$var_utmhn.'&utmr='.$var_referer.'&utmp='.$var_utmp.'&utmac='.$var_utmac.'&utmcc=__utma%3D'.$var_cookie.'.'.$var_random.'.'.$var_today.'.'.$var_today.'.'.$var_today.'.2%3B%2B__utmb%3D'.$var_cookie.'%3B%2B__utmc%3D'.$var_cookie.'%3B%2B__utmz%3D'.$var_cookie.'.'.$var_today.'.2.2.utmccn%3D(direct)%7Cutmcsr%3D(direct)%7Cutmcmd%3D(none)%3B%2B__utmv%3D'.$var_cookie.'.'.$var_uservar.'%3B';
?>

<!-- Google analytics JS disabled plugin -->
<noscript>
  <img src=""<?php echo $urchinUrl ?>" style="display: none;" />
</noscript>
<?php
}

function google_noscript_admin_menu() {
  if (function_exists('add_options_page')) {
    add_options_page('No Script Analytics', 'No Script Analytics', 8, basename(__FILE__), 'google_noscript_panel');
  }
}

add_action('admin_menu', 'google_noscript_admin_menu'); 
add_action('wp_footer', 'google_noscript');

?>

