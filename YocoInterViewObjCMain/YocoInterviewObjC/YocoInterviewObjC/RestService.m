//
//  RestService.m
//  YocoInterviewObjC
//
//  Created by Rohan Jansen on 2015/02/12.
//  Copyright (c) 2015 Rohan Jansen. All rights reserved.
//

#import "RestService.h"

@implementation RestService

-(NSString*) getBusinessCategories : (NSString*) reqURLStr
{
    NSURLRequest *Request = [NSURLRequest requestWithURL:[NSURL URLWithString: reqURLStr]];
    
    NSURLResponse *resp = nil;
    NSError *error = nil;
    
    NSData *response = [NSURLConnection sendSynchronousRequest: Request returningResponse: &resp error: &error];
    NSString *responseString = [[NSString alloc] initWithData:response encoding:NSUTF8StringEncoding];
    
    NSLog(@"%@",error.domain);
    NSLog(@"%@",responseString);
    
    return responseString;
}

@end
