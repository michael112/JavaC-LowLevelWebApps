import java.net.ServerSocket;
import java.net.InetAddress;
import java.net.Socket;
import java.io.InputStream;
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.List;
import java.util.ArrayList;
import java.nio.charset.StandardCharsets;
import java.io.OutputStream;
import java.io.PrintWriter;
import java.io.IOException;
import java.util.Scanner;

public class Main {

    public static void main( String[] args ) {
        try {
            int port = 8080;
            ServerSocket server = new ServerSocket(port);
            while(true) {
                System.out.println("Waiting for a connection (listening on port " + port + ")... ");
                Socket client = server.accept();
                InputStream stream = client.getInputStream();
                BufferedReader reader = new BufferedReader(new InputStreamReader(stream));
                String requestContent = "";
                String requestLine = null;
                while( ( requestLine == null ) || !( requestLine.equals("") ) ) {
                    requestLine = reader.readLine();
                    requestContent += requestLine + "\n";
                }
                System.out.println("<request>\n" + requestContent + "</request>");
                OutputStream output = client.getOutputStream();
                PrintWriter printWriter = new PrintWriter(output, true);
                String responseContent = "";
                responseContent += "HTTP/1.1 200 OK" + "\n";
                responseContent += "Content-Type: application/json" + "\n";
                responseContent += "\n";
                responseContent += "{" + "\n" + "\"imie\": \"Michał\"," + "\n" + "\"nazwisko\": \"Choromański\"," + "\n" + "\"wydzial\": \"Elektryczny\"," + "\n" + "\"kierunek\": \"Informatyka stosowana\"," + "\n" + "\"poziom\": \"magisterskie\"" + "\n" + "}";
                responseContent += "\n";
                printWriter.print(responseContent);
                printWriter.flush();
                System.out.print(new String(("<response>\n" + responseContent + "\n</response>\n").getBytes(), "UTF8"));
                client.close();
            }
        }
        catch( IOException e ) {
            System.out.printf("Exception: %s", e);
        }
        System.out.println("\nPress Enter to continue...");
        new Scanner(System.in).nextLine();
    }

}
